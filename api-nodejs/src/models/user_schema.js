import mongoose from "mongoose";
import { isEmail } from "validator";

const UserSchema= mongoose.Schema({

    nombre:{type:String, require:true},
    correo:{
        type: String, 
        unique:true, 
        required:true,
        validate: [isEmail, 'invalid email']
    },
    clave:{type: String, required:true, minLenght:8},
    rolId:{type:mongoose.Schema.Types.ObjectId, ref:'rol', required:true},
    activo:{type:Boolean, default:true}
},
{
    collection:'usuario',
    timestamps:{createdAt:'fechaCreacion'}
});

export const User = mongoose.model('usuario', UserSchema);
