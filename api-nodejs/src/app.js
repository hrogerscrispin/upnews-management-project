import express from 'express';
const app = express();

app.use(express.json());


//definir rutas

//middlewares
//app.use('/public',express.static(`${__dirname}/storage/imgs`))


export {app}