This is a small project to demonstrate some uses of [Artillery.io](https://www.artillery.io/docs)
There are two scripts:
- [artillery-pass](./artillery-pass.yml)
- [artillery-fail](./artillery-fail.yml)

To run the scripts you first must install [Artillery.io](https://www.artillery.io/docs/get-started/get-artillery)
```bash
npm install -g artillery@latest
```

Run an artillery scrip by using
```bash
artillery run artillery-pass.yml 

or 

artillery run artillery-pass.yml
```

You may need to change the ``` target: "http://localhost:5215" ``` in each script if you run the api on a different port.