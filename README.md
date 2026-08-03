# Aplicación de aprendizaje Movi-info

En esta aplicación se van incorporando poco a poco todo el aprendizaje que voy adquiriendo sobre:
- Front
- Arquitecturas
- Patrones de diseño
- Back
- Pruebas automáticas: Unitarias, Infraestructura y Aceptación

Info:
  - Web: https://movie-info.rubenalvarezgonzalez.eu
  - Imágenes Docker:
    - Front: https://hub.docker.com/repository/docker/rubenag83/movi-info-react-dotnet-frontend
    - Back: https://hub.docker.com/repository/docker/rubenag83/movi-info-react-dotnet-backend

## Despliegue

La aplicación se despliega en dos capas gratuitas, ambas conectadas a este repositorio: cada
push a `main` vuelve a desplegar de forma automática.

```
movie-info.rubenalvarezgonzalez.eu   (DNS en IONOS)
      |
      v
   Netlify  ---- sirve el resultado estático de "vite build"
      |
      +-- /api/*  --proxy-->  Render  (contenedor .NET 8)
                                 |
                                 v
                        api.themoviedb.org
```

El back **no necesita subdominio propio**: vive detrás del proxy de Netlify (`netlify.toml`).
Al resolverse todo bajo el mismo origen desde el punto de vista del navegador, no entra en
juego el CORS y la URL de Render nunca se expone.

### Puesta en marcha

El orden importa: Netlify hace de proxy hacia Render, así que el back tiene que existir antes.

1. **Render (back).** En https://dashboard.render.com/blueprints, "New Blueprint Instance" y
   elegir este repositorio. Render lee `render.yaml` y pide el valor de
   `TheMovieDB__Authorisation`: ahí va el token regenerado. Al terminar, anotar la URL que
   asigna (`https://<nombre>.onrender.com`) y comprobar que responde en `/health`.

   Si esa URL **no** es exactamente `https://movi-info-api.onrender.com`, hay que corregir el
   destino del proxy en `netlify.toml`, porque Render añade un sufijo cuando el nombre ya está
   cogido por otra cuenta.

2. **Netlify (front).** "Add new site" → "Import an existing project" → GitHub → este
   repositorio. No hay que rellenar nada del formulario de build: Netlify lee `netlify.toml` y
   toma de ahí el directorio base, el comando y la carpeta a publicar.

3. **Dominio.** En Netlify, "Domain management" → "Add a domain" →
   `movie-info.rubenalvarezgonzalez.eu`. Después, en el panel DNS de IONOS, crear:

   | Tipo | Nombre | Valor |
   |---|---|---|
   | CNAME | `movie-info` | `<nombre-del-sitio>.netlify.app` |

   El nombre del registro es solo la etiqueta del subdominio (`movie-info`), no el dominio
   completo: IONOS ya añade la zona. El certificado HTTPS lo emite Netlify por Let's Encrypt en
   cuanto el DNS propaga, sin intervención.

El plan gratuito de Render duerme el servicio tras 15 minutos sin tráfico, así que la primera
petición después de un rato de inactividad puede tardar cerca de un minuto en responder
mientras el contenedor arranca. Las siguientes van a velocidad normal.

### Configuración

| Dónde | Fichero | Qué hace |
|---|---|---|
| Netlify | `netlify.toml` | Build del front, proxy `/api/*`, fallback de SPA y cabeceras |
| Render | `render.yaml` | Servicio Docker del back, plan gratuito y healthcheck en `/health` |
| GitHub | `.github/workflows/ci.yml` | Compila y prueba front y back, y construye la imagen Docker |

### Variables de entorno

| Variable | Dónde se define | Para qué |
|---|---|---|
| `TheMovieDB__Authorisation` | Panel de Render (secreto) | Token de TheMovieDB. .NET traduce `__` a `:`, así que sobreescribe `TheMovieDB:Authorisation` de `appsettings.json` |
| `FrontEndHostName` | Panel de Render (opcional) | Orígenes extra permitidos por CORS, separados por comas |
| `VITE_DOT_NET_BACK` | `netlify.toml` / `.env.production` | Ruta del back. En producción es `/api/`, relativa, para que la resuelva el proxy |

El token **nunca se versiona**. Para ejecutar el back en local:

```bash
cd back-dotnet/MoviInfoBack
dotnet user-secrets set "TheMovieDB:Authorisation" "<token>" --project Web.API
```

Si el token falta, el back aborta el arranque con un mensaje explícito en lugar de responder
errores en cada petición.

### Notas de seguridad

- El token de TheMovieDB estuvo versionado en este repositorio público, así que **hay que
  regenerarlo** en TheMovieDB (Settings → API → *API Read Access Token*). Quitarlo del código
  no basta: sigue existiendo en el historial de git.
- `npm audit` reporta dos avisos abiertos en `react-router-dom`. Se mantiene la última versión
  publicada (7.18.2) a propósito: el aviso restante afecta únicamente al modo RSC, que esta SPA
  no usa, mientras que la versión que propone `npm audit` reintroduce un *open redirect* en
  `<Link>` y `useNavigate`, que sí se usan.

## Front
Las tecnologías utilizadas para la capa de Front son:
- JavaScript
- TypeScript
- React
- Vite
- Docker (docker-compose, network, build)
- NGINX
- Jest (pruebas de componente)

## Back
Las tecnologías utilizadas para la capa de Back son:
- DotNet Core 8
- LinQ
- Docker (docker-compose, network, build)
- API Rest
- xUnit con AutoFixture para pruebas Unitarias y de Infraestructura
- xUnit.Gherkin.Quick para pruebas de Aceptación
- CRQS con MediatR

La arquitectura que se está implementando es la Hexagonal, una arquitectura limpia que permite reducir el esfuerzo en caso de actualización del framework, entre otras ventajas.

Además se están aplicando los siguientes patrones de diseño:
- **Patrón DDD**: siempre acompañando a la arquitectura hexagonal donde primamos el dominio frente a la infraestructura. Organizando el esquema de carpetas desde los términos del dominio y siendo éstos siempre protagonistas.
- **Patrón Repository**: para facilitar el cambio de fuente de datos, se inicia con el acceso a la fuente de datos de una API de manera directa, a futuro se hará un Back y habrá que llamar a éste.
- **Patrón Criteria**: para mejorar la flexibilidad y minimizar el número de métodos a implementar para filtros y paginados de listados.

## Trabajos realizados
Aquí expondré qué cosas he incorporado en el código:
- [x] HTML Sintáctico
- [x] CSS3
  - [x] Herencia
  - [x] Modo **día/noche**
  - [x] Efectos cuando te colocas encima de un elmento del listado
- [x] Acceso a una API con autenticación (con fetch)
- [x] Listado de elementos Movile-First (Responsive)
- [x] Scroll infinito cargando datos desde la API (componente reutilizable)
- [x] Filtrado de contenidos del listado (componente reutilizable)
- [x] Footer fijo con la última página cargada y el número total de páginas.
- [x] Poner bonito el mensaje de "Cargando..."
- [x] Dar estilos y poblar de información la página de 1 película.
- [x] Construir un Back de acceso a la información de TheMovieDB
- [x] Crear el repositorio en el Front y conectarlo con el Back (poner a prueba DDD y la arquitectura hexagonal) :bowtie:
- [x] Dockerizar Front y Back
- [x] Conectarlos de manera que pudieran pasar por diferentes entornos sin volver a construir la imagen :bowtie:
- [x] Publicar la aplicación
- [x] Pruebas de componente en Front
- [x] Pruebas unitarias en Back
- [x] Pruebas de infraestructura en Back
- [x] Pruebas de aceptación en Back
- [x] Llamada al repositorio por Inyección de Dependencias
- [x] Llamada a los casos de uso desde los controladores por Querys con el patrón CQRS
- [ ] Crear una gestión de usuarios
- [ ] El usuario pueda marcar películas favoritas
- [ ] En detalles se va un icono de película favorita
- [ ] En el listado general aparezca un icono cuando es una película favorita
- [ ] Listado / Filtro de películas favoritas
- [ ] Poder sacar una película de favorita
