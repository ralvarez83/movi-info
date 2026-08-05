# Notas para Claude Code

## El proyecto

Aplicación de información de películas, con el front y el back separados:

| Carpeta | Qué es |
|---|---|
| `front-react/` | SPA de React + Vite en TypeScript. Arquitectura hexagonal en `src/Contexts/` |
| `back-dotnet/MoviInfoBack/` | API en .NET 8. Proyectos `Web.API`, `Movies`, `Shared` y `Test` |

El back es el único que habla con TheMovieDB; el front no lleva token ninguno.

Dentro de `front-react/src/Contexts/movies/infraestruture/` hay dos repositorios: `dotNetBack/`,
que es el que se usa, y `theMovieDb/`, que **es código muerto** de cuando el front atacaba TMDB
directamente. Nadie lo importa: `App.tsx` solo construye `DotNetBackRepository`. Conviene no
tomarlo como referencia y, si estorba, borrarlo.

## Comandos

```bash
# Front
cd front-react
npm ci
npm test                      # jest
npm run build                 # tsc && vite build
npm run dev                   # vite, modo development

# Back
cd back-dotnet/MoviInfoBack
dotnet restore
dotnet build --configuration Release
dotnet test --configuration Release --no-build \
  --filter "FullyQualifiedName!~Test.Movies.Infraestructure&FullyQualifiedName!~Test.WebAPI"
```

Ese filtro de pruebas es el mismo que usa el CI, y hay que respetarlo en un entorno limpio:

- `Test.Movies.Infraestructure` ataca la API real de TheMovieDB y necesita credenciales.
- `Test.WebAPI` son de aceptación y necesitan una instancia escuchando en `localhost:5021`.

Para arrancar el back en local hace falta el token, o el arranque aborta a propósito:

```bash
PORT=10000 TheMovieDB__Authorisation="<token v4 de TMDB>" dotnet run
```

## Despliegue

```
movie-info.rubenalvarezgonzalez.eu   (zona DNS en IONOS, CNAME -> movi-info.netlify.app)
      |
      v
   Netlify        sirve el estático de "vite build"
      |
      +-- /api/*  --proxy-->  Render  (contenedor Docker con el back)
                                 |
                                 v
                        api.themoviedb.org
```

Todo queda bajo el mismo origen desde el punto de vista del navegador, así que **no entra en
juego el CORS** y la URL de Render nunca se expone.

Puntos que no son evidentes y ya han costado un rato:

- **Netlify no construye el proyecto.** Publica GitHub Actions con `netlify-cli` (job `deploy`
  de `.github/workflows/ci.yml`), así que Netlify no llega a leer `netlify.toml`. El proxy, el
  fallback de SPA y las cabeceras viven en `front-react/public/_redirects` y
  `front-react/public/_headers`, que Vite copia a `dist/` y viajan en el artefacto.
- **`netlify-cli` exige Node >=22.12**, pero el front se construye con la 20. El job cambia de
  versión entre el build y el despliegue justamente por eso.
- **El token de TMDB va sin el prefijo `Bearer`.** El código lo antepone (`AuthorisationType`
  vale `"Bearer"` en `appsettings.json`). Con el prefijo puesto, la cabecera sale duplicada y
  TMDB responde 401 a todo, pero `/health` sigue devolviendo 200 porque no llama a TMDB. Para
  comprobar que el token es bueno hay que pedir `/api/movies/550`.
- **Render duerme el servicio** tras 15 minutos sin tráfico en el plan gratuito. La primera
  petición después puede tardar cerca de un minuto. No es un fallo.

Los pasos de alta completos están en el README.

## Entorno de las sesiones cloud

El SDK de .NET **no viene preinstalado**, y la política de red por defecto bloquea
`builds.dotnet.microsoft.com`, que es de donde descarga `dotnet-install.sh`:

```
CONNECT builds.dotnet.microsoft.com:443 -> HTTP/1.1 403 Forbidden
```

No hace falta pelearse con eso: `dotnet-sdk-8.0` está en los repos de Ubuntu 24.04, que sí
están permitidos. Instalarlo así funciona y da la misma versión 8.0.1xx que pide el CI:

```bash
apt-get update && apt-get install -y dotnet-sdk-8.0
```

Merece la pena hacerlo al empezar si hay que tocar el back, porque permite compilar y ejecutar
las pruebas de verdad en vez de depender solo del CI.

Lo que **no** se puede hacer desde una sesión cloud, porque el proxy los bloquea:

| Bloqueado | Consecuencia |
|---|---|
| `*.onrender.com`, `*.netlify.app`, `app.netlify.com` | No se puede verificar el despliegue por HTTP |
| `movie-info.rubenalvarezgonzalez.eu` y el dominio raíz | Las pruebas de punta a punta las tiene que lanzar el usuario |
| `api.github.com` por `curl` | Hay que usar las herramientas MCP de GitHub |

Las consultas DNS **sí** funcionan (`dig` va por el puerto 53, directo), así que la propagación
de un cambio de DNS sí se puede comprobar desde aquí. `dig` no viene instalado:
`apt-get install -y dnsutils`.

Docker tiene CLI pero **no hay demonio**, de modo que las imágenes solo se construyen en CI.

## Pendientes conocidos, que no bloquean

- **El lint está roto de antes.** `.eslintrc.cjs` extiende `standard-with-typescript`, que no
  está instalado, así que `npm run lint` falla. Se dejó fuera del CI a propósito.
- **Dos warnings CS8618** de nulabilidad en `Test/WebAPI/Movies/`. Preexistentes.

## Vulnerabilidades

Ahora mismo no hay ninguna abierta, y conviene que siga así:

```bash
cd front-react && npm audit
cd back-dotnet/MoviInfoBack && dotnet list package --vulnerable --include-transitive
```

Dos cosas del arreglo que hay que tener presentes para no deshacerlas sin querer:

- El front va con **React 19 y `react-router` 8** (no `react-router-dom`, que se quedó en la 7
  y no tiene versión sin el aviso). `react-router` 8 exige **Node >=22.22.0**, que es la
  versión que fijan el CI y `netlify.toml`; bajarla rompería el despliegue.
- `react-router` 8 se publica **solo como ESM** y su punto de entrada arrastra código de SSR
  con `import.meta`, que jest no sabe cargar. Por eso la prueba de `MovieDetails` sustituye el
  módulo entero con `jest.mock` en vez de importarlo. Si aparece
  `Cannot use 'import.meta' outside a module` en otra prueba, la salida es la misma: mockear el
  módulo, no intentar que jest digiera la librería.
