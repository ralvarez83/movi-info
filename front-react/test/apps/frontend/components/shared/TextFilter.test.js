import React from 'react';
import ReactDOM from 'react-dom';
import { fireEvent, render, screen } from "@testing-library/react";
import {expect, test, jest} from '@jest/globals';
import { TextFilter } from '../../../../../src/apps/frontend/components/shared/TextFilter'
import { FilterOperator } from '../../../../../src/Contexts/Shared/Domain/Criteria/Filters/FilterTypes.d'


describe("Input text to filter", () => {
  it("calls with more caracteres than minimal (3 by default)", () =>{
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

    const filter = screen.getByLabelText(placeholder);
    fireEvent.change(filter, {
      target: { value: expectedFilter.value },
    });

    expect(setFilter).toHaveBeenCalledWith(expectedFilter);  
  })

  it("doesn't call with less caracteres than minimal (3 by default)", () =>{
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

    const filter = screen.getByLabelText(placeholder);
    fireEvent.change(filter, {
      target: { value: expectedFilter.value },
    });

    expect(setFilter).not.toHaveBeenCalled();  
  })
})