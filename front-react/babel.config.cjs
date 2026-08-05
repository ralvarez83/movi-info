// Configuración de Babel para jest. Vite no la usa: compila con SWC.
//
// Era un .babelrc, que Babel trata como configuración "relativa al fichero" y por tanto no
// alcanza a nada que esté fuera de este paquete. Como configuración de proyecto sí aplica a
// todo lo que jest le pase, incluidas las dependencias, que es lo que hace falta si alguna vez
// hay que transformar algo de node_modules con transformIgnorePatterns.
//
// El fichero es .cjs a propósito: el paquete declara "type": "module", así que un
// babel.config.js se interpretaría como ESM y no admitiría module.exports.
module.exports = {
  presets: [
    '@babel/preset-env',
    ['@babel/preset-react', { runtime: 'automatic' }],
    '@babel/preset-typescript'
  ]
}
