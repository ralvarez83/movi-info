import React from 'react';
import { fireEvent, render, screen } from "@testing-library/react";
import {expect, jest} from '@jest/globals';
import { TextFilter } from '../../../../../src/apps/frontend/components/shared/TextFilter'
import { FilterOperator } from '../../../../../src/Contexts/Shared/Domain/Criteria/Filters/FilterTypes.d'

describe ("TextFilter", ()=> {
  describe("Write more than minimal length (3 by defaul)", () => {
    it("should call the filter handler", () =>{
      const setFilter = jest.fn();
      const placeholder = "busca"
      const emptyFilter = {
        field: "textFinder",
        operator: FilterOperator.CONTAINS,
        value: ''
      }
  
      const expectedFilter = {
        field: "textFinder",
        operator: FilterOperator.CONTAINS,
        value: 'Lor'
      }
  
      render (<TextFilter setFilter={setFilter} placeholder={placeholder} filter={emptyFilter} />)
  
      const filter = screen.getByLabelText(/busca/i);
      fireEvent.change(filter, {
        target: { value: expectedFilter.value },
      });
  
      expect(setFilter).toHaveBeenCalledWith(expectedFilter);  
    })
  })
  describe("Write less than minimal length (3 by defaul)", () => {
    it("shouldn't call theh filter handler", () =>{
      const setFilter = jest.fn();
      const placeholder = "busca"
      const emptyFilter = {
        field: "textFinder",
        operator: FilterOperator.CONTAINS,
        value: ''
      }
  
      const expectedFilter = {
        field: "textFinder",
        operator: FilterOperator.CONTAINS,
        value: 'Lo'
      }
  
      render (<TextFilter setFilter={setFilter} placeholder={placeholder} filter={emptyFilter} />)
  
      const filter = screen.getByLabelText(/busca/i);
      fireEvent.change(filter, {
        target: { value: expectedFilter.value },
      });
  
      expect(setFilter).not.toHaveBeenCalled();  
    })
  })
})