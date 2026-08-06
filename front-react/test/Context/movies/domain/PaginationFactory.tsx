import {Pagination} from "../../../../src/Contexts/Shared/Domain/Criteria/Pagination"
import { faker } from "@faker-js/faker";
import {Factory} from "fishery"

const paginationFactory = Factory.define<Pagination>(({ sequence }) => ({
  page : sequence,
  totalPage: faker.number.int(),
  // El doble representa una única página de resultados. Antes isLastPage devolvía un booleano
  // al azar, así que el scroll infinito seguía pidiendo páginas un número indeterminado de
  // veces y la prueba dependía de la suerte: con el repositorio devolviendo siempre la misma
  // respuesta, cada vuelta añadía otra vez las mismas películas.
  isLastPage : () => true,
  getNextPage: () => {return paginationFactory.build()}
}));

export function generatePaginationRandom(params){
  return paginationFactory.build(params);
}
