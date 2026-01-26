import 'dotenv/config'; 

export const config={
    appConfig:{
        host: process.env.APP_HOST,
        port: process.env.PORT
    },
    dbConfig:{
        uri: process.env.MONGO_URI
    }
}

