@focus
Feature: Pipeline overview Layout

    This function check if all components are loaded onto the Pipeline overview page
    Background: Login as a manager
    Given A manager is logged in
    And I open the pipelineOverview page

    Scenario Outline: Pipeline overview contains <item>
    Then the pipelineOverview containst <item>

    Examples:
    |item|
    |"header"|
    |"sidebar"|
    |"mainWindow"|

    
