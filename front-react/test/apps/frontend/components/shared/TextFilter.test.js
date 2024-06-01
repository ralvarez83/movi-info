import React from 'react';
import ReactDOM from 'react-dom';
import { fireEvent, render, screen } from "@testing-library/react";
import {expect, test, jest} from '@jest/globals';
import { TextFilter } from '../../../../../src/apps/frontend/components/shared/TextFilter'
import { FilterOperator } from '../../../../../src/Contexts/Shared/Domain/Criteria/Filters/FilterTypes.d'


describe("Input text to filter", () => {
  it("calls with more caracteres than minimal (3 by default)", () =>{
    const setFilter = jest.fn();
    const placeholder = "mi placeholder"
    const emptyFilter = {
      field: "text finder",
      operator: FilterOperator.CONTAINS,
      value: ''
    }

    render (<TextFilter setFilter={setFilter} placeholder={placeholder} filter={emptyFilter} />)

    
  })
})