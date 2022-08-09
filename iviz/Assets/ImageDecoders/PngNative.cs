// This code is mostly taken from https://github.com/qmfrederik/libpng-sharp with minor changes

using System;
using System.Runtime.InteropServices;
using Iviz.Core;

namespace Iviz.ImageDecoders
{
    /// <summary>
    /// The color types of a PNG image.
    /// </summary>
    [Flags]
    public enum PngColorType : byte
    {
        /// <summary>
        /// The image uses a color palette.
        /// </summary>
        Palette = 1,

        /// <summary>
        /// The image uses true colors.
        /// </summary>
        Color = 2,

        /// <summary>
        /// The image has an alpha channel.
        /// </summary>
        Alpha = 4,

        /// <summary>
        /// The image is a grayscale image.
        /// </summary>
        Gray = 0,

        /// <summary>
        /// The image uses a color palette.
        /// </summary>
        ColorPalette = (Color | Palette),

        /// <summary>
        /// The image uses a RGB pixel format.
        /// </summary>
        RGB = Color,

        /// <summary>
        /// The image uses a RGBA pixel format.
        /// </summary>
        RGBA = (Color | Alpha),

        /// <summary>
        /// The image uses a grayscale and has an alpha channel.
        /// </summary>
        GrayAlpha = (Alpha),
    }    
    
    /// <summary>
    /// Specifies options for adding a filler byte to a pixel.
    /// </summary>
    public enum PngFillerFlags : int
    {
        /// <summary>
        /// Adds the filler at the start of the pixel array.
        /// </summary>
        Before = 0,

        /// <summary>
        /// Adds the filler at the end of the pixel array.
        /// </summary>
        After = 1
    }    
    
    /// <summary>
    /// Provides access to native methods for interacting with libpng.
    /// </summary>
    internal static class PngNative
    {
        /// <summary>
        /// The name of the libpng library, excluding any platform-dependent prefixes (such as <c>lib</c>) and suffixes (such as <c>.so</c>).
        /// </summary>
        const string Library =
            Settings.IsMobile
                ? "__Internal"
                : "png16";

        /// <summary>
        /// Gets the library version string.
        /// </summary>
        /// <param name="png_ptr">
        /// A pointer to an instance of libpng. This can be <see cref="IntPtr.Zero"/>.
        /// </param>
        /// <returns>
        /// The library version as a short string in the format <c>1.0.0</c> through <c>99.99.99zz</c>
        /// </returns>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr png_get_libpng_ver(IntPtr png_ptr);

        /// <summary>
        /// Allocate and initialize a png_struct structure for reading PNG file
        /// </summary>
        /// <param name="user_png_ver">
        /// Version string of the library. Must be <c>PNG_LIBPNG_VER_STRING</c>
        /// </param>
        /// <param name="error_ptr">
        /// User-defined struct for error functions.
        /// </param>
        /// <param name="error_fn">
        /// User-defined function for printing errors and aborting.
        /// </param>
        /// <param name="warn_fn">
        /// User-defined function for warnings.
        /// </param>
        /// <returns>
        /// Returns the pointer to png_struct structure. Returns <see cref="IntPtr.Zero"/> if it fails to create the structure.
        /// </returns>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr png_create_read_struct(IntPtr user_png_ver, IntPtr error_ptr, png_error error_fn, png_error warn_fn);

        /// <summary>
        /// Allocate and initialize a png_info structure
        /// </summary>
        /// <param name="png_ptr">
        /// </param>
        /// <returns>
        /// Returns the pointer to png_info structure. Returns <see cref="IntPtr.Zero"/> if it fails to create the structure.
        /// </returns>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr png_create_info_struct(IntPtr png_ptr);

        /// <summary>
        /// Set user-defined function for reading a PNG stream
        /// </summary>
        /// <param name="png_ptr">
        /// Pointer to input data structure png_struct
        /// </param>
        /// <param name="io_ptr">
        /// Pointer to user-defined structure containing information about the input functions. This value may be <see cref="IntPtr.Zero"/>.
        /// </param>
        /// <param name="read_data_fn">
        /// Pointer to new input function that shall take the following arguments:
        /// - a pointer to a png_struct
        /// - a pointer to a structure where input data can be stored
        /// - 32-bit unsigned int to indicate number of bytes to read
        /// The input function should invoke png_error() to handle any fatal errors and png_warning() to handle non-fatal errors.
        /// </param>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_read_fn(IntPtr png_ptr, IntPtr io_ptr, [MarshalAs(UnmanagedType.FunctionPtr)]png_rw read_data_fn);

        /// <summary>
        /// Reads the information before the actual image data from the PNG file. The function allows reading a file that already has the PNG signature bytes read from the stream.
        /// </summary>
        /// <param name="png_ptr"></param>
        /// <param name="info_ptr"></param>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_read_info(IntPtr png_ptr, IntPtr info_ptr);

        /// <summary>
        /// Returns the image width in pixels.
        /// </summary>
        /// <param name="png_ptr"></param>
        /// <param name="info_ptr"></param>
        /// <returns>
        /// The image width in pixels.
        /// </returns>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint png_get_image_width(IntPtr png_ptr, IntPtr info_ptr);

        /// <summary>
        /// Returns the image height in pixels.
        /// </summary>
        /// <param name="png_ptr"></param>
        /// <param name="info_ptr"></param>
        /// <returns>
        /// Returns 0 if png_ptr or info_ptr is <see cref="IntPtr.Zero"/>, image_height otherwise.
        /// </returns>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint png_get_image_height(IntPtr png_ptr, IntPtr info_ptr);

        /// <summary>
        /// Returns the image color type.
        /// </summary>
        /// <param name="png_ptr"></param>
        /// <param name="info_ptr"></param>
        /// <returns>
        /// Returns 0 if png_ptr or info_ptr is NULL, color_type otherwise.
        /// </returns>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern PngColorType png_get_color_type(IntPtr png_ptr, IntPtr info_ptr);

        /// <summary>
        /// Get number of color channels in image
        /// </summary>
        /// <param name="png_ptr"></param>
        /// <param name="info_ptr"></param>
        /// <returns>
        /// On success, png_get_channels() shall return the number of channels ranging from 1-4. Otherwise, png_get_channels shall return 0.
        /// </returns>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte png_get_channels(IntPtr png_ptr, IntPtr info_ptr);

        /// <summary>
        /// Return image bit_depth
        /// </summary>
        /// <param name="png_ptr"></param>
        /// <param name="info_ptr"></param>
        /// <returns>
        /// Returns 0 if png_ptr or info_ptr is NULL, bit_depth otherwise.
        /// </returns>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte png_get_bit_depth(IntPtr png_ptr, IntPtr info_ptr);

        /// <summary>
        /// Set the scheme to interlacing for writing an image and return the number of sub-images required to write the image.
        /// </summary>
        /// <param name="png_ptr"></param>
        /// <returns>
        /// 7 if the image is interlaced, otherwise 1.
        /// </returns>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int png_set_interlace_handling(IntPtr png_ptr);

        /// <summary>
        /// Updates the structure pointed to by info_ptr to reflect any transformations that have been requested.
        /// For example, rowbytes will be updated to handle expansion of an interlaced image with png_read_update_info().
        /// </summary>
        /// <param name="png_ptr"></param>
        /// <param name="info_ptr"></param>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_read_update_info(IntPtr png_ptr, IntPtr info_ptr);

        /// <summary>
        /// Read the entire image into memory. For each pass of an interlaced image, use png_read_rows() instead.
        /// </summary>
        /// <param name="png_ptr"></param>
        /// <param name="image"></param>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_read_image(IntPtr png_ptr, IntPtr image);

        /// <summary>
        /// Return number of bytes for a row
        /// </summary>
        /// <param name="png_ptr"></param>
        /// <param name="info_ptr"></param>
        /// <returns>
        /// Returns 0 if <paramref name="png_ptr"/> or <paramref name="info_ptr"/> is <see cref="IntPtr.Zero"/>, number of bytes otherwise.
        /// </returns>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint png_get_rowbytes(IntPtr png_ptr, IntPtr info_ptr);

        /// <summary>
        /// Reads the end of a PNG file after reading the image data, including any comments or time information at the end of the file.
        /// The function shall not read past the end of the file.
        /// </summary>
        /// <param name="png_ptr"></param>
        /// <param name="info_ptr"></param>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_read_end(IntPtr png_ptr, IntPtr info_ptr);

        /// <summary>
        /// Frees the memory associated with the read png_struct struct that holds information from the given PNG file, the associated png_info struct for
        /// holding the image information and png_info struct for holding the information at end of the given PNG file.
        /// </summary>
        /// <param name="png_ptr_ptr"></param>
        /// <param name="info_ptr_ptr"></param>
        /// <param name="end_info_ptr_ptr"></param>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_destroy_read_struct(ref IntPtr png_ptr_ptr, ref IntPtr info_ptr_ptr, ref IntPtr end_info_ptr_ptr);

        /// <summary>
        /// Frees the memory pointed to by <paramref name="png_ptr"/> previously allocated by png_malloc().
        /// </summary>
        /// <param name="png_ptr"></param>
        /// <param name="ptr"></param>
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_free(IntPtr png_ptr, IntPtr ptr);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_palette_to_rgb(IntPtr png_ptr);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void png_set_gray_1_2_4_to_8(IntPtr png_ptr);
        public static extern void png_set_expand_gray_1_2_4_to_8(IntPtr png_ptr);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_strip_16(IntPtr png_ptr);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_strip_alpha(IntPtr png_ptr);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_invert_alpha(IntPtr png_ptr);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_add_alpha(IntPtr png_ptr);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_filler(IntPtr png_ptr, uint filler, PngFillerFlags flags);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_packing(IntPtr png_ptr);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_bgr(IntPtr png_ptr);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_swap_alpha(IntPtr png_ptr);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_gray_to_rgb(IntPtr png_ptr);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_invert_mono(IntPtr png_ptr);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_swap(IntPtr png_ptr);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_packswap(IntPtr png_ptr);

        public delegate void png_rw(IntPtr png_ptr, IntPtr outBytes, uint byteCountToRead);

        public delegate void png_error(IntPtr png_structp, IntPtr png_const_charp);
        
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int png_sig_cmp(IntPtr png_ptr, int start, int num_to_check);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern void png_set_sig_bytes(IntPtr png_ptr, int num_bytes);
        
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr png_get_io_ptr(IntPtr png_ptr);
    }
}