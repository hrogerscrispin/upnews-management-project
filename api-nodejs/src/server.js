import 'dotenv/config';
import {app} from '../src/app.js'
import {config} from '../src/config.js'
const {appConfig}= config
import {mongoConnection} from '../src/infrastructure/db/mongodb_config.js'

//llamar conexion a BD
mongoConnection();

//levantando puerto
const {port} = appConfig || 5000

app.listen(port,()=>{
    console.log("App corriendo en el puerto: ", port);
});
