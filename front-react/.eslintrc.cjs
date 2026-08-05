// Configuración de ESLint del front.
//
// Vive aquí y no en la raíz del repositorio porque "npm run lint" se ejecuta desde esta
// carpeta, y es la única del proyecto con código JavaScript o TypeScript.
//
// Antes extendía "standard-with-typescript", que no estaba instalado y hacía fallar el lint
// entero. Ese paquete además está descatalogado: se renombró a "eslint-config-love". En lugar
// de traerlo, la configuración se apoya en lo que el proyecto ya declara como dependencia, que
// es lo que se venía usando de verdad.
module.exports = {
  root: true,
  env: {
    browser: true,
    es2021: true
  },
  extends: [
    'eslint:recommended',
    'plugin:@typescript-eslint/recommended',
    'plugin:react/recommended',
    'plugin:react-hooks/recommended'
  ],
  parser: '@typescript-eslint/parser',
  parserOptions: {
    ecmaVersion: 'latest',
    sourceType: 'module'
  },
  plugins: ['@typescript-eslint', 'react', 'react-refresh'],
  settings: {
    react: {
      version: 'detect'
    }
  },
  ignorePatterns: ['dist', 'coverage', 'node_modules'],
  rules: {
    // Con el runtime automático de JSX no hace falta importar React en cada fichero.
    'react/react-in-jsx-scope': 'off',
    // Los tipos los pone TypeScript; PropTypes duplicaría el trabajo.
    'react/prop-types': 'off',
    'react-refresh/only-export-components': ['warn', { allowConstantExport: true }]
  },
  overrides: [
    {
      // Las pruebas corren en jest, que aporta sus propios globales.
      files: ['test/**/*.{js,jsx,ts,tsx}'],
      extends: ['plugin:jest/recommended'],
      env: {
        jest: true
      }
    },
    {
      // Los ficheros de configuración son CommonJS y se ejecutan en Node.
      files: ['*.cjs'],
      env: {
        node: true
      },
      parserOptions: {
        sourceType: 'script'
      }
    }
  ]
}
