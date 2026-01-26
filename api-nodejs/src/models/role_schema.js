import mongoose from 'mongoose';

const RoleSchema = new mongoose.Schema({

    nombre:{
        type:String,
        required:true
    },
    permisos:{
        type:[mongoose.Schema.Types.ObjectId],
        ref:'permiso',
        required:true,
        default:[]
    }


},{
    collection:'rol'
});

export const Role = mongoose.model('rol',RoleSchema);