import {type Movie} from "../../../../src/Contexts/movies/domain/Movie"
import { faker } from "@faker-js/faker";
import crypto from 'crypto';
import {Factory} from "fishery"

const videoFactory = Factory.define<Movie>(({ sequence }) => ({
  id: sequence.toString(),
  title: faker.lorem.word({ length: { min: 5, max: 7 }, strategy: 'fail' }) ,
  usersVote: faker.number.float({ multipleOf: 0.25, min: 0, max:10 }),
  overview: faker.lorem.paragraph(),
  imdbLink: 'https://www.imdb.com/title/tt' + faker.number.int(),
  adult: (crypto.randomInt(100) % 2) === 0,
  verticalImagePath: faker.image.urlLoremFlickr(),
  horizontalImagePath: faker.image.urlLoremFlickr()
}));

export function generateVideoRandom(params){
  return videoFactory.build(params);
}

export function generateVideoRandonList(min = 0, max = 10) {
  const length = Math.random() * (max - min) + min;
  return videoFactory.buildList(length);
}