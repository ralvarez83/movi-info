import {type Movie} from "../../../../src/Contexts/movies/domain/Movie"
import {jest} from '@jest/globals';

export class VideoMother {
  
  public static Random (): Movie {
    
    jest.mock("../../../../src/Contexts/movies/domain/Movie")

    const movie : jest.Mocked<Movie>;

    // const movie = {
    //   adult:;
    //   verticalImagePath:;
    //   horizontalImagePath:;
    //   id: ;
    //   overview: ;
    //   title:;
    //   imdbLink:;
    //   usersVote:;
    //  }
  }

}