import {expect, test, jest} from '@jest/globals';
import { render, screen, waitForElementToBeRemoved } from "@testing-library/react";
import "@testing-library/jest-dom";
import { axe, toHaveNoViolations } from "jest-axe";
import React from 'react';
import { DotNetBackRepository } from '../../../../src/Contexts/movies/infraestruture/dotNetBack/DotNetBackRepository';
import { MovieSearchResults } from '../../../../src/Contexts/movies/domain/Movie';
import { generateMovieSearchResultRandom } from '../domain/MovieSearchResultFactory';
import {MovieListInfinite} from '../../../../src/apps/frontend/components/MovieListInfinite'
import { mockIntersectionObserver } from '../infraestructure/mockIntersectionObserver';
import { truncate } from './utils/text';

jest.mock("../../../../src/Contexts/movies/infraestruture/dotNetBack/DotNetBackRepository");

expect.extend(toHaveNoViolations);

describe('MovieListInfinite', () => {
  describe('When access', () => {
    describe ('at first time', () => {
      test("should be accesible", async () => {
        let moviRepo = new DotNetBackRepository("");
        const movieResult = generateMovieSearchResultRandom({}) as MovieSearchResults

        (moviRepo.searchByCriteria as jest.Mock).mockResolvedValue(movieResult as never)
      
        const [intersectionObserver] = mockIntersectionObserver([true]);
        const { container } = render (<MovieListInfinite repository={moviRepo} />)
        await waitForElementToBeRemoved(() => screen.getByText(/cargando/i))
        expect(await axe(container)).toHaveNoViolations();
      })

      test("should show the first page movies", async () => {
        let moviRepo = new DotNetBackRepository("");
        const movieResult = generateMovieSearchResultRandom({}) as MovieSearchResults

        (moviRepo.searchByCriteria as jest.Mock).mockResolvedValue(movieResult as never)
      
        const [intersectionObserver] = mockIntersectionObserver([true]);
        const { container } = render (<MovieListInfinite repository={moviRepo} />)
        await waitForElementToBeRemoved(() => screen.getByText(/cargando/i))
        screen.debug
        
        movieResult.movies.forEach(movie => {
          const titleTruncate = truncate(movie.title, 25)
          const overviewTruncate = truncate(movie.overview, 100)
          expect(screen.getByText(titleTruncate)).toBeInTheDocument();
          expect(screen.getByText(overviewTruncate)).toBeInTheDocument();
        });

        (moviRepo.searchByCriteria as jest.Mock).mockRestore();
        (moviRepo.searchByCriteria as jest.Mock).mockReset();
        (moviRepo.searchByCriteria as jest.Mock).mockClear();
      })
    })
  });
})

