import {expect, test, jest} from '@jest/globals';
import { render, screen, waitForElementToBeRemoved } from "@testing-library/react";
import "@testing-library/jest-dom";
import { axe, toHaveNoViolations } from "jest-axe";
import React from 'react';
import { useParams } from "react-router";
import {MovieDetails} from '../../../../src/apps/frontend/components/MovieDetails'
import { DotNetBackRepository } from '../../../../src/Contexts/movies/infraestruture/dotNetBack/DotNetBackRepository';
import { Movie } from '../../../../src/Contexts/movies/domain/Movie';
import { generateMovieRandom } from '../domain/MovieFactory';

jest.mock("../../../../src/Contexts/movies/infraestruture/dotNetBack/DotNetBackRepository");

// react-router se publica solo como ESM y su punto de entrada arrastra el código de SSR, que
// usa import.meta para detectar el HMR de Vite y que jest no sabe cargar. Esta prueba solo
// necesita decidir qué devuelve useParams, así que se sustituye el módulo entero en vez de
// hacer que jest digiera una parte de la librería que el componente ni siquiera usa.
// __esModule es imprescindible: sin esa marca, la interoperabilidad de babel envuelve el mock
// en una copia, y el componente acabaría viendo un objeto distinto del que se configura aquí,
// con lo que useParams devolvería undefined.
jest.mock("react-router", () => ({
  __esModule: true,
  useParams: jest.fn()
}));

expect.extend(toHaveNoViolations);

describe('MovieDetails', () => {
  describe('Find a existing Movie', () => {
    test('should show it info', async () => {
  
      const moviRepo = new DotNetBackRepository("");
      const movie: Movie | undefined = generateMovieRandom({}) as Movie;
  
      (moviRepo.findById as jest.Mock).mockResolvedValue(movie as never);

      (useParams as jest.Mock).mockReturnValue({ id: movie.id });
      
      Object.defineProperty(window, 'matchMedia', {
        writable: true,
        value: jest.fn().mockImplementation(query => ({
          matches: false,
          media: query,
          onchange: null,
          addListener: jest.fn(), // Deprecated
          removeListener: jest.fn(), // Deprecated
          addEventListener: jest.fn(),
          removeEventListener: jest.fn(),
          dispatchEvent: jest.fn(),
        })),
      });
  
      const { container } = render (<MovieDetails repository={moviRepo} />)
  
      await waitForElementToBeRemoved(() => screen.getByText(/cargando/i))
  
      expect(await axe(container)).toHaveNoViolations();
  
      const title = screen.getByText(movie.title)
      const imdb = screen.getByLabelText("Link de acceso a la página de IMDB de la película")
      
      const overview = screen.getByText(movie.overview)
  
      expect(title).toBeInTheDocument();
      expect(imdb.href).toContain(movie.imdbLink);
      expect(overview).toBeInTheDocument();
      (moviRepo.findById as jest.Mock).mockClear();
    });
  })

  describe('Find a non existing Movie', () => {
    test('should show an error message', async () => {
      const moviRepo = new DotNetBackRepository("");
      const movie: Movie | undefined = undefined;
  
      (moviRepo.findById as jest.Mock).mockResolvedValue(movie as never);

      (useParams as jest.Mock).mockReturnValue({ id: Math.random().toString() });
      
      Object.defineProperty(window, 'matchMedia', {
        writable: true,
        value: jest.fn().mockImplementation(query => ({
          matches: false,
          media: query,
          onchange: null,
          addListener: jest.fn(), // Deprecated
          removeListener: jest.fn(), // Deprecated
          addEventListener: jest.fn(),
          removeEventListener: jest.fn(),
          dispatchEvent: jest.fn(),
        })),
      });
  
      const { container } = render (<MovieDetails repository={moviRepo} />)
      await waitForElementToBeRemoved(() => screen.getByText(/cargando/i))
  
      expect(await axe(container)).toHaveNoViolations();
  
      const message = screen.getByText(/no encontrada/i)
  
      expect(message).toBeInTheDocument()
    });
  })  
});
