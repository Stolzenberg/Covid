## Covid News Scraper API
### Little application project

Searches predefined web pages for articles that contain keywords, then puts the article headings and a direct link into a model and sends it back as a json array.

**Design Patterns and Principles**
- Commands
- Dependency Injection
- Services & Repositories
- Single Responsibility Principle

**Example how it would look like:**
```
{
    "header": "COVID19 live updates Moderna vaccine gets full FDA approval",
    "link": "https://www.medicalnewstoday.com/articles/live-updates-coronavirus-covid-19"
},
{
    "header": "The neurological impact of COVID19 What we know so far",
    "link": "https://www.medicalnewstoday.com/articles/the-neurological-impact-of-covid-19-what-we-know-so-far"
},
{
    "header": "Study finds x27no adverse association between COVID19 vaccination and",
    "link": "https://www.medicalnewstoday.com/articles/study-finds-no-adverse-association-between-covid-19-vaccination-and-fertility"
},
{
    "header": "Silent epidemic of SARSCoV2 in whitetailed deer raises concerns",
    "link": "https://www.medicalnewstoday.com/articles/silent-epidemic-of-sars-cov-2-in-white-tailed-deer-raises-concerns"
}
```
