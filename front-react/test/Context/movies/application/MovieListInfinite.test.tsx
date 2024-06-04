import {expect, test, jest} from '@jest/globals';
import { act, render, screen, waitForElementToBeRemoved } from "@testing-library/react";
import "@testing-library/jest-dom";
import { axe, toHaveNoViolations } from "jest-axe";
import React from 'react';
import * as react_router from "react-router";
import { DotNetBackRepository } from '../../../../src/Contexts/movies/infraestruture/dotNetBack/DotNetBackRepository';
import { Movie, MovieList, MovieSearchResults } from '../../../../src/Contexts/movies/domain/Movie';
import { generateMovieSearchResultRandom } from '../domain/MovieSearchResultFactory';
import {MovieListInfinite} from '../../../../src/apps/frontend/components/MovieListInfinite'
import { mockIntersectionObserver } from '../infraestructure/mockIntersectionObserver';
import { Pagination } from '../../../../src/Contexts/Shared/Domain/Criteria/Pagination';

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
        
        const title = screen.getByText(movieResult.movies[0].title)

        expect(title).toBeInTheDocument();
        (moviRepo.searchByCriteria as jest.Mock).mockRestore();
        (moviRepo.searchByCriteria as jest.Mock).mockReset();
        (moviRepo.searchByCriteria as jest.Mock).mockClear();
      })
    })
  });
})

