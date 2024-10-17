Feature: Overview of the pipelines

    Feature Description

    Scenario: Is the organization list displayed?

    Given I log in as a user with the username "test" and the password "test"
    Then I validate that navigation bar contains message "Pipeline Processing for dummies group D"
    And i validate that the list "Organization" is displayed
    And i validate that the list "Organization" contains the name "DTU"

    Scenario: Is the repository list displayed?

    Given I log in as a user with the username "test" and the password "test"
    Then I validate that navigation bar contains message "Pipeline Processing for dummies group D"
    And i validate that the list "Repositories" is displayed
    And i validate that the list "Repositories" contains the name "Test"