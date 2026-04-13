import mongoose from "mongoose";


const StatusSchema = new mongoose.Schema({

    nombre:{type: String, require: true},
    codigo:{type: String, require: true},
    descripcion:{type: String, maxLength: 60, require: true}
},{
    collection:'estadoNoticia',
    timestamps:{createdAt:'fechaCreacion'}
});


export const Status = mongoose.model('estadoNoticia', StatusSchema);