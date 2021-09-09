
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
#import json

#import requests
from sklearn.preprocessing import PolynomialFeatures
from sklearn.linear_model import LinearRegression
from sklearn.pipeline import Pipeline


def polynomialregression(xtrain, ytrain, xprediction, x_daynumbers, degree, startdaypredict, period, startperioddate):
    model = Pipeline([('poly', PolynomialFeatures(degree=degree)),
                      ('linear', LinearRegression(fit_intercept=False))])

    x = np.array(xtrain[startdaypredict : ]).reshape(-1, 1).tolist()
    y = np.array(ytrain[startdaypredict : ]).reshape(1, -1)[0].tolist()
    model = model.fit(x, y)
    yprediction = model.predict(xprediction).astype(int)

    lastdaynum = x_daynumbers[len(x_daynumbers) - recorddaynumber]

    xpredictionperiod = np.arange(lastdaynum, lastdaynum + period).reshape(-1, 1)
    ypredictionperiod = model.predict(xpredictionperiod).astype(int)

    datelist = pd.date_range(startperioddate, periods = period).tolist()
    resultData = pd.DataFrame({'date' : datelist, 'cases' : ypredictionperiod})
    
    return (resultData, yprediction, xpredictionperiod, ypredictionperiod)


def preprocessing(data, recorddaynumber):
    location = data[countrycolname].values
    forecastingdata = data[forecastingcolname].values
    daydata = data[dayname].values
    
    y_forecastingcases = []
    x_daynumbers = []

    for i in range(0, len(location)):
        if(location[i] == countryname):
            y_forecastingcases.append(forecastingdata[i])
            x_daynumbers.append(daydata[i])

    xtrain = x_daynumbers[ : len(x_daynumbers) - recorddaynumber]
    ytrain = y_forecastingcases[ : len(x_daynumbers) - recorddaynumber]
    xprediction = np.array(x_daynumbers[len(x_daynumbers) - recorddaynumber : ]).reshape(-1, 1)
    
    return (x_daynumbers, y_forecastingcases, xtrain, ytrain, xprediction)

def loaddata(filename):
    data = pd.read_csv(filename)
    data = pd.DataFrame(data)
    data = data.fillna(0)
    data[datecolname] = data[datecolname].astype('category')
    data[dayname] = data[datecolname].cat.codes
    
    return data

filename = 'WHO-COVID-19-global-data.csv'
datecolname = 'Date_reported'
countrycolname = 'Country'
forecastingcolname = 'Cumulative_cases' # Cumulative_cases 'Cumulative_deaths'
countryname = 'Canada'
dayname = 'Day'
recorddaynumber = 146 #146 116
startdayimage = 720
degree = 3
startdaypredict = 753 #753 781
period = 32
startperioddate = '24/01/2022'


data = loaddata(filename)

(x_daynumbers,
 y_forecastingcases,
 xtrain, 
 ytrain, 
 xprediction) = preprocessing(data,
                              recorddaynumber)

(resultData, 
 yprediction, 
 xpredictionperiod, 
 ypredictionperiod) = polynomialregression(xtrain,
                                           ytrain,
                                           xprediction, 
                                           x_daynumbers, 
                                           degree, 
                                           startdaypredict, 
                                           period, 
                                           startperioddate)


plt.rcParams["figure.figsize"] = (12, 7)

plt.plot(x_daynumbers[startdayimage : startdayimage+100], 
         y_forecastingcases[startdayimage : startdayimage+100], 
         color='cornflowerblue', 
         linewidth = 2,
         label = countryname + " cases COVID19")

plt.plot(xtrain[startdayimage:startdayimage+100], 
         ytrain[startdayimage:startdayimage+100], 
         color = 'navy', 
         linewidth = 3,
         label = "Training points")

plt.plot(xprediction[:50], 
         yprediction[:50], 
         color = 'C' + str(degree), 
         linewidth = 2,
         label = "Polynomial regression (degree=%d)" % degree)

plt.plot(xpredictionperiod[:50], 
         ypredictionperiod[:50], 
         color = 'green', 
         linewidth = 3, 
         label = "Forecasting")

plt.legend(loc = 'best')
plt.savefig('foo.png', bbox_inches='tight')
plt.show()


print(resultData)


#df1['date'] = df1['date'].apply(lambda x: x.strftime('%Y/%m/%d'))
#df1['infected'] = df1['infected'].apply(str)
#to_json = df1.to_json()
#print(df1)
#print(to_json)
#with open('prediction.json', 'w') as f:
    #json.dump(to_json, f)
    
    




