Feature: GetAMovie
	In order to choose a movie
	As a movie viewer
	I want to read the movie details

Scenario: Access to a movie details
	Given I send a GET request to '/api/movies/653346'
  Then the response status code should be 200
	And the result should 'El reino del planeta de los simios'

Scenario: Access to a wrong movie id
	Given I send a GET request to '/api/movies/-21'
  Then the response status code should be 404