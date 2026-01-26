import mongoose from "mongoose";
import 'dotenv/config';
import { config } from "../../config.js";
const {dbConfig} = config

export const mongoConnection = async()=>{
    try{
        const {uri} = dbConfig 
        await mongoose.connect(uri);
        console.log("Connection Succesfull!");
        console.log(`connected to the ${mongoose.connection.name} database`);
    }
    catch(error){
        console.log("error trying to connect to mongodb: ",error.name);
    }
}