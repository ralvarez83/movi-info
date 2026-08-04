# Notas para Claude Code

## Entorno cloud: el SDK de .NET no viene preinstalado

Las sesiones de Claude Code en la nube (claude.ai/code) **no traen el SDK de .NET**.
Sin configurar nada, `dotnet` no está en el PATH y el back no se puede compilar ni testear.

Además, el nivel de red por defecto (**Trusted**) **bloquea `builds.dotnet.microsoft.com`**,
que es de donde descarga `dotnet-install.sh`. Comprobado:

```
CONNECT builds.dotnet.microsoft.com:443 -> HTTP/1.1 403 Forbidden
```

El bloqueo es del proxy del entorno, no de Microsoft: `dotnet.microsoft.com` sí está en la
lista Trusted, pero no hay comodín que cubra el subdominio `builds.`.

### Solución recomendada: setup script con apt (sin tocar la red)

`dotnet-sdk-8.0` está en los repos oficiales de Ubuntu 24.04, que ya están permitidos en
Trusted (`archive.ubuntu.com`). Basta con poner esto en el campo **Setup script** del
entorno (claude.ai/code -> icono de nube -> engranaje):

```bash
#!/bin/bash
apt update && apt install -y dotnet-sdk-8.0 || true
```

La versión que ofrece Ubuntu (8.0.1xx) coincide con el `8.0.x` que pide el CI
(`.github/workflows/ci.yml`).

### Alternativa: abrir el dominio

Solo hace falta si se necesita una versión que Ubuntu no empaquete (p. ej. .NET 9):

1. En el diálogo del entorno, **Network access** -> **Custom**.
2. En **Allowed domains**: `*.dotnet.microsoft.com`
3. **Marcar** "Also include default list of common package managers" — si no, la lista
   propia *sustituye* a la de Trusted y se pierden nuget.org, npm, Ubuntu, etc.
4. Setup script:

```bash
#!/bin/bash
curl -sSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh
bash /tmp/dotnet-install.sh --channel 8.0 --install-dir /usr/share/dotnet || true
ln -sf /usr/share/dotnet/dotnet /usr/local/bin/dotnet
```

Los cambios de entorno **solo aplican a sesiones nuevas**, no a la que ya está corriendo.

### Qué sí funciona sin configurar nada

`api.nuget.org` (200) y GitHub (proxy aparte) son accesibles, así que restaurar paquetes
NuGet no es el problema — el bloqueo está únicamente en obtener el SDK.

## Pruebas del back

El CI ejecuta solo las pruebas unitarias, excluyendo dos grupos que no corren en un runner
limpio (ver comentario en `.github/workflows/ci.yml`):

```
dotnet test --configuration Release --no-build \
  --filter "FullyQualifiedName!~Test.Movies.Infraestructure&FullyQualifiedName!~Test.WebAPI"
```

- `Test.Movies.Infraestructure` ataca la API real de TheMovieDB (necesita credenciales).
- `Test.WebAPI` son de aceptación y necesitan una instancia en `localhost:5021`.

Usa ese mismo filtro al ejecutar pruebas en una sesión cloud.
