import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'

// https://vitejs.dev/config/
//
// Antes esto exportaba una función que recibía "mode" y lo ignoraba, devolviendo siempre esta
// misma configuración. La firma además era incorrecta: Vite llama a esa función con un objeto
// ({ command, mode, ... }), no con una cadena. Las variables del front las carga Vite por su
// cuenta desde los ficheros .env según el modo, así que no hace falta ni la función ni loadEnv.
export default defineConfig({
  plugins: [react()]
})
