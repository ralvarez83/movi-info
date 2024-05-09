# Aplicación de aprendizaje Movi-info

En esta aplicación se van incorporando poco a poco todo el aprendizaje que voy adquiriendo sobre:
- Front
- Arquitecturas
- Patrones de diseño

## Front
Las tecnologías utilizadas para la capa de Front son:
- JavaScript
- TypeScript
- React

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