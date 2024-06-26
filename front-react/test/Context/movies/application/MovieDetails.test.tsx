import {expect, test, jest} from '@jest/globals';
import { render, screen, waitForElementToBeRemoved } from "@testing-library/react";
import "@testing-library/jest-dom";
import { axe, toHaveNoViolations } from "jest-axe";
import React from 'react';
import * as react_router from "react-router";
import {MovieDetails} from '../../../../src/apps/frontend/components/MovieDetails'
import { DotNetBackRepository } from '../../../../src/Contexts/movies/infraestruture/dotNetBack/DotNetBackRepository';
import { Movie } from '../../../../src/Contexts/movies/domain/Movie';
import { generateMovieRandom } from '../domain/MovieFactory';

jest.mock("../../../../src/Contexts/movies/infraestruture/dotNetBack/DotNetBackRepository");

expect.extend(toHaveNoViolations);

describe('MovieDetails', () => {
  describe('Find a existing Movie', () => {
    test('should show it info', async () => {
  
      let moviRepo = new DotNetBackRepository("");
      const movie: Movie | undefined = generateMovieRandom({}) as Movie;
  
      (moviRepo.findById as jest.Mock).mockResolvedValue(movie as never)
  
      jest.spyOn(react_router, "useParams").mockReturnValueOnce({ id: movie.id });
      
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
      let moviRepo = new DotNetBackRepository("");
      const movie: Movie | undefined = undefined;
  
      (moviRepo.findById as jest.Mock).mockResolvedValue(movie as never)
  
      jest.spyOn(react_router, "useParams").mockReturnValueOnce({ id: Math.random().toString() });
      
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
