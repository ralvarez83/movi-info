import React, { ReactNode, useEffect, useRef } from 'react'

interface Props {
  // El componente no mira dentro de la lista: solo la usa como dependencia del efecto, para
  // volver a observar cuando cambia. Por eso unknown y no un tipo concreto de dominio.
  dataList: unknown,
  getMoreData: () => void
}

type PropsWithChildren<P> = P & { children?: ReactNode };


// Las props se desestructuran aquí, y no dentro del efecto, para poder listarlas de una en una
// como dependencias: usar props.loQueSea dentro obligaría a depender del objeto props entero,
// que cambia en cuanto cambia cualquier prop.
export const InfinitePagination: React.FC<PropsWithChildren<Props>> = ({ dataList, getMoreData, children }) => {
  const observerTargetEndPage = useRef(null);

  useEffect(() => {
    // Definido dentro del efecto: fuera se recreaba en cada render y no podía figurar como
    // dependencia. Lo único que necesita de fuera es getMoreData, que useMoviesState memoriza.
    const onIntersection = (entries: IntersectionObserverEntry[]) => {
      const firstEntry = entries[0]
      if (firstEntry.isIntersecting) getMoreData();
    }

    const observer = new IntersectionObserver(onIntersection);

    if(observer && observerTargetEndPage.current) observer.observe(observerTargetEndPage.current);

    return () => {
      if (observer) observer.disconnect();
    };
  }, [dataList, getMoreData])

  return (
    <>
      {children}
      <div ref={observerTargetEndPage}></div>
    </>
  )
}