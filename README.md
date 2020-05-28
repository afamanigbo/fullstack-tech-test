![Bridge International Academies Logo](Banner%20Logo%20280x60.png)

# Fullstack Developer Technical Test

## Instructions

* Write your solution in C# (esp. dotnet core).
* Use any non-commercial external libraries you deem necessary, as long as you include the necessary tooling and instructions to make installing them easy peasy.
* Assume the presence of standard package managers (esp. NuGet) - please do not include the third party packages themselves in your submission.
* Include automated unit tests, as and where you deem them appropriate, given the problem statement.
* Remember that just because a test uses NUnit or XUnit or MSTest, that does not make it a unit test. (you may also include integration tests, if you find those helpful)
* Do not include any executables, binaries, or installers.
* Include a README.md that provides:
    * Instructions on how to run the application (including both tests and the service)
    * An explanation of your design and assumptions
    * Any outstanding scope or room for improvement

As a general rule, we allow three days from the date that you receive these instructions and have a kick-off call in order to submit your code, though you may request more time if needed. If you have any questions about the exercise, as it relates to your interview process, please contact us.

## Opinionated Bridge Opinions

* There are a lot of ways to write software and many of them are good. At Bridge, we have our share of opinions about how we like to build software that you might want to take into consideration as you work on this exercise:
Good naming matters. A lot.
* “When you feel the need to write a comment, first try to refactor the code so that any comment becomes superfluous.” --Kent Beck
* Being easily testable is itself a design objective - if code is hard to test, refactor it until it is easy to test.

## Evaluation Process
We assess a number of things, including the design of your solution, program correctness, coding style, user consideration, and general maintainability. While it is a small-ish problem, we expect you to submit what you believe is production-quality code – code that you’d be able to run, maintain, and evolve. You don’t need to “gold plate” your solution; however we are looking for something more than a quick and dirty prototype.
You should not need to spend more than 2 hours total on this exercise to produce a great solution. However, as you plan your work, if you are considering how to trade off between scope and quality, you should err on the side of quality. For any scope that you have cut, please list it in your README, along with a high-level strategy for how you would continue the work.

## Problem - Teacher Tablet Battery Usage
### The task
From time to time, our in-country tech teams are called upon to perform school visits to address  issues that they find. We collect a bunch of data on an on-going basis about these devices and we’re interested in building a tool to identify teacher tablet batteries that are in need of replacement.

Your task is to write a web service that provides the average daily battery usage for each tablet across the period, which will be used by various web and mobile clients.

### Inputs and Requirements

* We’re providing a JSON data file that represents a week’s worth of battery data for a subset of schools in a given country.
    * Each element represents a reading at a single point in time for a particular device
    * The employeeID is a unique identifier for each employee. It tells you who was logged into the device when the relevant measurement was taken - but doesn’t affect the battery calculation. It’s possible that different users are using the same device.
    * serialNumber represents a unique device and the basis of the calculation
* Integrate with the input file like you would against any API, including any relevant service layers - just don’t worry about any of the network or security aspects that a real API would entail. We’ve done this to keep things simple.
* A battery is considered to be in need of replacement if it uses more than 30% of its battery per day (on average). These are e-ink devices that are expected to last a week or more between charges.
Calculations should span all available data points for a given tablet, irrespective of the day.
* There is no reason to assume that data will be recorded at the same time on two separate days. Power levels are only recorded when the device touches the network or some other event triggers.
* All intervals should be weighted by duration (time)
    * If there were only two readings for a given device, one at 9 AM reading 100% and one at 9 PM on the same day reading 90%, then the average daily battery usage would be 20% (10% over 12 hours == 20% over 24 hours).
    * If there were then a third reading at 9 PM for the following day at 80% (and no other readings), then the daily average for that device would be 13.3% (20% over 36 hours).
* If the battery level increases between measurements, assume that the device was charged between readings and the change should be excluded from the calculation.
* If there were a fourth reading at 10 PM at 100%, the daily average for that device would remain at 13.3%.
ALL data points for a given device across the week’s data should be considered together.
* If there is only a single data point for a given device in the data set, the battery usage is “unknown”.
