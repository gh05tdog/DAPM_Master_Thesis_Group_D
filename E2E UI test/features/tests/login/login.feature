Feature: Login Feature 

    Feature Description

    Scenario: Log in with existing user

    Given I open a main page
    Then I validate redirection to the KeycloakLoginPage
    When I type "test" value in the "username" field
    And I type "test" value in the "password" field
    And I submit by clicking the login button
    Then I validate that navigation bar contains message "Pipeline Processing for dummies group D"

    Scenario: Log in with wrong credentials

    Given I open a main page
    Then I validate redirection to the KeycloakLoginPage
    When I type "qwewqeqweqwertytrytrytr" value in the "username" field
    And I type "qwewqeqweqwertytrytrytr" value in the "password" field
    And I submit by clicking the login button
    And I print a message to the console
    Then I validate that alert message "Invalid username or password." is shown