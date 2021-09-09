from sklearn.cluster import AgglomerativeClustering
from scipy.spatial import ConvexHull
import pandas as pd
import matplotlib.pyplot as plt
import numpy as np

def encircle(x,y, ax=None, **kw):
    if not ax: ax=plt.gca()
    p = np.c_[x,y]
    hull = ConvexHull(p)
    poly = plt.Polygon(p[hull.vertices,:], **kw)
    ax.add_patch(poly)

df = pd.read_csv('data.csv',sep=';')
print(df)

cluster = AgglomerativeClustering(n_clusters=3, affinity='euclidean', linkage='ward')  
cluster.fit_predict(df[['Earnings', 'Age','Congestion','Joy']])  

plt.figure(figsize=(8, 5), dpi= 80)  
plt.scatter(df.iloc[:,0], df.iloc[:,1], c=cluster.labels_, cmap='tab10')  
  
encircle(df.loc[cluster.labels_ == 0, 'Earnings'], 
         df.loc[cluster.labels_ == 0, 'Age'], ec="k", fc="tab:green", alpha=0.2, linewidth=0)
encircle(df.loc[cluster.labels_ == 1, 'Earnings'], 
         df.loc[cluster.labels_ == 1, 'Age'], ec="k", fc="tab:blue", alpha=0.2, linewidth=0)
encircle(df.loc[cluster.labels_ == 2, 'Earnings'], 
         df.loc[cluster.labels_ == 2, 'Age'], ec="k", fc="tab:red", alpha=0.2, linewidth=0)

plt.xlabel('Profit'); plt.xticks(fontsize=12)
plt.ylabel('Age'); plt.yticks(fontsize=12)
plt.title('Dependence of age on income', fontsize=22)
plt.show()