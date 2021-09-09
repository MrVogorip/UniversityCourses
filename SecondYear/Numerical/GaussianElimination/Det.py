def Det(S):
    a, n= 1,len(S)
    for i in range(n):
        a*=S[i][i]
        buf=S[i][i]
        for j in range(n):
            S[i][j]/=buf
        j=i+1
        while j<n:
            buf=S[j][i]
            for k in range(n):
                S[j][k]-=S[i][k]*buf
            j+=1
    return str(a)