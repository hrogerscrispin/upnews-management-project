import mongoose from 'mongoose';


const PermissionSchema = new mongoose.Schema({

    codigo:{
        type:String,
        required:true
    },
    descripcion:{
        type:String,
        required:true,
        maxLenght: 30
    }

},

    {
        collection:'permiso'
    }

);

export const Permission = mongoose.model('permiso', PermissionSchema);