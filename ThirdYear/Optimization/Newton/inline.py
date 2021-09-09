from scipy.optimize import minimize
rz = lambda x: 4*(x[0])**2+8*x[0]-x[1]-3;
h_1 = lambda x: (-x[0] - x[1] - 2);
h_2 = lambda x: (0.0);
x0 = [0, 0];
cons = ({'type': 'ineq', 'fun': h_1},
       {'type': 'ineq', 'fun': h_2}) 

print(minimize(rz, x0, constraints=cons))
x_c = [0, 0]
i = 1
k = 1
c = 10
eps = 0.001
while i < 1000:
        curr_func = lambda x: rz(x) + c**i*k*(h_1(x)**2 + h_2(x)**2)
        x_c = minimize(curr_func, x_c).x;
        i += 1
        if curr_func(x_c) < eps:
            break
print(x_c)
print(i)