# My_Blog-s-Sample
Using IdentityServer to implement SSO login mode in ASP.NET Core is very simple



### Grpc Service Bulk injection Flowchart

```flow
st=>start: 用户登陆
op=>operation: 登陆操作
cond=>condition: 登陆成功 Yes or No?
e=>end: 进入后台

st->op->cond
cond(yes)->e
cond(no)->op
```