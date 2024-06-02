import {type Movie} from "../../../../src/Contexts/movies/domain/Movie"
import crypto from 'crypto';


export class VideoMother {
  
  public static Random (): Movie {
    
    const randomMovie: Movie = {
      id: crypto.randomInt(100000).toString(),
      title: crypto.randomBytes(10).toString('hex'),
      usersVote: 72,
      overview: crypto.randomBytes(50).toString('hex'),
      imdbLink: 'https://www.imdb.com/title/tt' + crypto.randomInt(100000),
      adult: (crypto.randomInt(100) % 2) === 0,
      verticalImagePath: crypto.randomBytes(10).toString('hex'),
      horizontalImagePath: crypto.randomBytes(10).toString('hex')
    }
    
    return randomMovie
  }

}