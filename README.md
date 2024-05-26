# Aplicación de aprendizaje Movi-info

En esta aplicación se van incorporando poco a poco todo el aprendizaje que voy adquiriendo sobre:
- Front
- Arquitecturas
- Patrones de diseño
- Back

Info:
  - Web: http://194.164.174.221:8181
  - Imágenes Docker:
    - Front: https://hub.docker.com/repository/docker/rubenag83/movi-info-react-dotnet-frontend
    - Back: https://hub.docker.com/repository/docker/rubenag83/movi-info-react-dotnet-backend

## Front
Las tecnologías utilizadas para la capa de Front son:
- JavaScript
- TypeScript
- React
- Vite
- Docker (docker-compose, network, build)
- NGINX

## Back
Las tecnologías utilizadas para la capa de Back son:
- DotNet Core 8
- LinQ
- Docker (docker-compose, network, build)
- API Rest

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
- [ ] Crear una gestión de usuarios
- [ ] El usuario pueda marcar películas favoritas
- [ ] En detalles se va un icono de película favorita
- [ ] En el listado general aparezca un icono cuando es una película favorita
- [ ] Listado / Filtro de películas favoritas
- [ ] Poder sacar una película de favorita
- 