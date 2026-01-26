import mongoose from 'mongoose';

const CategorySchema = new mongoose.Schema({
    
    nombreCategoria:{
        type:String,
        required:true,
        maxLenght:15
    },
    descripcion:{
        type:String,
        required:true,
        maxLenght:30
    }
},{
    collection:'categoria'
});

export const category = mongoose.model('categoria',CategorySchema);
