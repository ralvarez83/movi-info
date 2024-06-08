Feature: SearchMovies
	In order to chose a movie to watch
	As a movie viewer
	I want to review what movies exists

Scenario: Review from all movies
	Given I send a GET request to '/api/movie/?page=1'
  Then the response status code should be 200
	And the result should return 20 movies

Scenario: Search a specific movie
	Given I send a GET request to '/api/movie/?byText=Casa&page=1'
  Then the response status code should be 200
	And all result should have the word 'Casa' or 'Home' in title or overview

Scenario: Search a non exists movie
	Given I send a GET request to '/api/movie/?byText=DFADCFE&page=1'
  Then the response status code should be 200
	And the result should return 0 movies