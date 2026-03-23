import mongoose from "mongoose";
import {config} from '../config.js'
const {appConfig } = config;

const NewSchema = new mongoose.Schema({

    titulo:{type: String, required: true},
    descripcion:{type: String, required: true, maxLength: 40},
    contenido:{type: String, required:true},
    portada:{type:String},
    autorId:{type:mongoose.Schema.Types.ObjectId, ref: 'usuario', required:true},
    categoriaId:{type:mongoose.Schema.Types.ObjectId, ref: 'categoria', required:true},
    paisId:{type:mongoose.Schema.Types.ObjectId, ref: 'pais', required:true}

},{
    collection:'noticia',
    timestamps:{createdAt:'fechaPublicacion'}
});

// the 'methods' function from mongoose allows the creation of a custom method regarding the actual schema.
// this specific one is used to somehow create the complete URL of a selected image to be correctly storage in our DB.
NewSchema.methods.setImgUrl = function setImgUrl(fileName){
   const {port, host} = appConfig;
   this.portada = `${host}:${port}/public/${fileName}`
}

export const News = mongoose.model('noticia', NewSchema);