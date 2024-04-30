import React, { ReactNode, useEffect, useRef } from 'react'
import { MovieList } from '../../../../Contexts/movies-info/movies/domain/Movie';

interface Props {
  dataList: MovieList,
  getMoreData: () => void
}

type PropsWithChildren<P> = P & { children?: ReactNode };


export const InfinitePagination: React.FC<PropsWithChildren<Props>> = (props: PropsWithChildren<Props>) => {
  const observerTargetEndPage = useRef(null);

  useEffect(() => {
    const observer = new IntersectionObserver(onIntersection);
    
    if(observer && observerTargetEndPage.current) observer.observe(observerTargetEndPage.current);
    
    return () => {
      if (observer) observer.disconnect();
    };
  }, [props.dataList])
  
  const onIntersection = async(entries:IntersectionObserverEntry[]) => {
    const firstEntry = entries[0]
    if (firstEntry.isIntersecting) props.getMoreData();
  }
    
  return (
    <>
      {props.children}
      <div ref={observerTargetEndPage}></div>
    </>
  )
}