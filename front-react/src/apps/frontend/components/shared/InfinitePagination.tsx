import React, { ReactNode, useEffect, useRef } from 'react'

interface Props {
  // El componente no mira dentro de la lista: solo la usa como dependencia del efecto, para
  // volver a observar cuando cambia. Por eso unknown y no un tipo concreto de dominio.
  dataList: unknown,
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
    // getMoreData falta a propósito en las dependencias. La función que llega desde
    // useMoviesState se recrea en cada render y captura movieList y pagination, así que
    // incluirla volvería a crear el IntersectionObserver continuamente. El efecto se apoya en
    // que dataList cambia justo después de cada carga, que es cuando toca volver a observar.
    // Arreglarlo en condiciones pasa por memorizar getMovies en el hook.
    // eslint-disable-next-line react-hooks/exhaustive-deps
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