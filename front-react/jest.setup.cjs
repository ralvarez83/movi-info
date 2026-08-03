// jsdom no expone TextEncoder/TextDecoder como globales, pero React Router 7 los usa al
// cargarse. Se toman de node:util, donde tienen la misma implementación que en el navegador.
const { TextEncoder, TextDecoder } = require('node:util')

global.TextEncoder = global.TextEncoder ?? TextEncoder
global.TextDecoder = global.TextDecoder ?? TextDecoder
