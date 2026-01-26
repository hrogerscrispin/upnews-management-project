import mongoose, { mongo } from "mongoose";

const CountrySchema = new mongoose.Schema({
    nombrePais:{type:String, required:true}
},{
    collection:'pais'
});

export const Country = mongoose.model('pais',CountrySchema);

