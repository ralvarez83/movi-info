import { BelongsToCollection } from "./movieAtributes/BelongsToCollection";
import { Genre } from "./movieAtributes/Genre";
import { ProductionCompany } from "./movieAtributes/ProductionCompany";
import { ProductionCountry } from "./movieAtributes/ProductionCountry";
import { SpokenLanguage } from "./movieAtributes/SpokenLanguage";

export interface Movie {
  adult:                 boolean;
  backdrop_path:         string;
  belongs_to_collection: BelongsToCollection;
  budget:                number;
  genres:                Genre[];
  homepage:              string;
  id:                    number;
  imdb_id:               string;
  origin_country:        string[];
  original_language:     string;
  original_title:        string;
  overview:              string;
  popularity:            number;
  poster_path:           string;
  production_companies:  ProductionCompany[];
  production_countries:  ProductionCountry[];
  release_date:          Date;
  revenue:               number;
  runtime:               number;
  spoken_languages:      SpokenLanguage[];
  status:                string;
  tagline:               string;
  title:                 string;
  video:                 boolean;
  vote_average:          number;
  vote_count:            number;
 }


export type MovieList = Movie[]