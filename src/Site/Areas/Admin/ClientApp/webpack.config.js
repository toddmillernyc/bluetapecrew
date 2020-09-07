var path = require('path');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const LiveReloadPlugin = require('webpack-livereload-plugin');

module.exports = {
    module: {
        rules: [
            {
                test: /\.css$/i,
                use: [
                    'style-loader',
                    'css-loader'
                ]
            },
            {
                test: /\.js/,
                use: 'babel-loader'
            },
            {
                test: /\.ico/,
                loader: 'file-loader',
                options: {
                    name: '[name].[ext]',
                    publicPath: 'shop'
                }
            },
            {
                test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
                loader: 'file-loader',
                options: {
                    name: './fonts/[name].[ext]'
                }
            },
            {
                test: /\.(png|jpe?g|gif)$/i,
                loader: 'file-loader',
                options: {
                    name: 'img/[name].[ext]',
                    publicPath: 'shop'
                }
            }
        ]
    },
    entry: {
        app: './src/index.js'
    },
    plugins: [
        new CleanWebpackPlugin(),
        new LiveReloadPlugin()
    ],
    output: {
        path: path.resolve(__dirname, '../../../wwwroot/admin/'),
        filename: 'bundle.js'
    }
};