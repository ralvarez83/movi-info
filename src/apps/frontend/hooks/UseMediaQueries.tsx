/* Based on: https://medium.com/@ttennant/react-inline-styles-and-media-queries-using-a-custom-react-hook-e76fa9ec89f6 */

import {useEffect, useState} from 'react';

export const useMediaQuery = (query: string) => {
  const mediaMatch = window.matchMedia(query);
  const [matches, setMatches] = useState(mediaMatch.matches);

  useEffect(() => {
    const handler = (e: { matches: boolean | ((prevState: boolean) => boolean); }) => setMatches(e.matches);
    mediaMatch.addEventListener("change",handler)
    return () => mediaMatch.removeEventListener("change",handler)
  });
  return matches;
};