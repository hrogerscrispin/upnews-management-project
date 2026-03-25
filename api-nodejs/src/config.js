import 'dotenv/config'; 
import dotenv from 'dotenv'
import path from 'path';
import { fileURLToPath } from 'url';


const __dirname = path.dirname(fileURLToPath(import.meta.url));

dotenv.config({path:path.join(__dirname,'../../.env')});

export const config={
    appConfig:{
        host: process.env.APP_HOST,
        port: process.env.PORT
    },
    dbConfig:{
        uri: process.env.MONGO_URI
    }
}

