Feature: Login

Scenario: Login with valid credentials
	Given Given a user is logged out
	When I login with username "admin" and password "secret"
	Then I have logged in

	Scenario: Login with invalid credentials
	Given Given a user is logged out
	When I login with username "admin" and password "12345"
	Then I have not logged in
