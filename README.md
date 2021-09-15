# YNAE
 YNAE is a .NET based emulator for the mildly obscure Yonni Na scripting language, also known as YNA
 
## What is YNA?
 YNA is a scripting language created and used mainly for user-generated content in the '42' Discord bot. 42 has the ability to save messages and links as 'tags' and recall them at any time, but in addition to this feature, 42 supports an original scripting language that it uses to provide dynamic and custom outputs, executing the code given by the users. As such, YNA is a flexible but limited and has quite an unusual syntax and systems compared to more popular programming languages, in addition to being closed source and as such difficult to test and learn about.
 
 ## What is YNAE?
  YNAE aims to fix one of the biggest issues of YNA: The lackluster ability to test your code.
  While 42 does provide a few decent debugging tools, it's just not enough most of the time, as such, I've decided to make an emulator to help provide a testing environment for people who might need it.
  As such, YNAE's design relies on 3 pillars:
### Feedback
YNAE attempts to provide the most explicit and useful feedback possible, from logging to errors, we want to give as much information as possible to the developpers.
### Flexibility
YNAE is an *emulator* not an interpreter. As such, it doesn't replicate 1:1 the features of YNA but instead gives you control of them, to test out any and all edge cases you're simply unable to usually
### Versatiliy
YNAE aims to provide enough tools to help develop and test as many YNA tags as possible, emulating features such as metadata and user data as best as it can without huring the other two pillars
