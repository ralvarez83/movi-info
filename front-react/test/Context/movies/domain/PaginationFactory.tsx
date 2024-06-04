import {Pagination} from "../../../../src/Contexts/Shared/Domain/Criteria/Pagination"
import { faker } from "@faker-js/faker";
import crypto from 'crypto';
import {Factory} from "fishery"

const paginationFactory = Factory.define<Pagination>(({ sequence }) => ({
  page : sequence,
  totalPage: faker.number.int(),
  isLastPage : () => {return faker.number.int() % 2 === 0},
  getNextPage: () => {return paginationFactory.build()}
}));

export function generatePaginationRandom(params){
  return paginationFactory.build(params);
}
