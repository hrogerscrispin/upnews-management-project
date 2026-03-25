import { User } from "../models/user_schema.js";
import bcrypt from "bcrypt";

// Crear un nuevo usuario
export const createUser = async (req, res) => {
  try {
    const { nombre, correo, clave } = req.body;

    // Validar que los campos requeridos están presentes
    if (!nombre || !correo || !clave) {
      return res.status(400).json({
        success: false,
        message: 'Faltan campos requeridos: nombre, correo, clave'
      });
    }

    // Validar longitud mínima de la contraseña
    if (clave.length < 8) {
      return res.status(400).json({
        success: false,
        message: 'La contraseña debe tener al menos 8 caracteres'
      });
    }

    // Verificar si el correo ya existe
    const usuarioExistente = await User.findOne({ correo });
    if (usuarioExistente) {
      return res.status(409).json({
        success: false,
        message: 'El correo ya está registrado'
      });
    }

    // Hash de la contraseña
    const claveHasheada = await bcrypt.hash(clave, 10);

    // Crear nuevo usuario
    const nuevoUsuario = new User({
      nombre,
      correo,
      clave: claveHasheada,
      activo: true
    });

    // Guardar en la base de datos
    await nuevoUsuario.save();

    // Obtener el usuario sin la contraseña
    const usuarioGuardado = await User.findById(nuevoUsuario._id)
      .select('-clave');

    res.status(201).json({
      success: true,
      message: 'Usuario creado correctamente',
      data: usuarioGuardado
    });
  } catch (error) {
    console.error('Error al crear usuario:', error);
    res.status(500).json({
      success: false,
      message: 'Error al crear usuario',
      error: error.message
    });
  }
};
// Obtener todos los usuarios
export const getAllUsers = async (req, res) => {
  try {
    const usuarios = await User.find()
      .select('-clave'); // Excluye el campo de contraseña

    if (!usuarios || usuarios.length === 0) {
      return res.status(404).json({
        success: false,
        message: 'No se encontraron usuarios',
        data: []
      });
    }

    res.status(200).json({
      success: true,
      message: 'Usuarios obtenidos correctamente',
      data: usuarios
    });
  } catch (error) {
    console.error('Error al obtener usuarios:', error);
    res.status(500).json({
      success: false,
      message: 'Error al obtener usuarios',
      error: error.message
    });
  }
};

// Obtener un usuario por ID
export const getUserById = async (req, res) => {
  try {
    const { id } = req.params;
    const usuario = await User.findById(id)
      .select('-clave');

    if (!usuario) {
      return res.status(404).json({
        success: false,
        message: 'Usuario no encontrado'
      });
    }

    res.status(200).json({
      success: true,
      message: 'Usuario obtenido correctamente',
      data: usuario
    });
  } catch (error) {
    console.error('Error al obtener usuario:', error);
    res.status(500).json({
      success: false,
      message: 'Error al obtener usuario',
      error: error.message
    });
  }
};
