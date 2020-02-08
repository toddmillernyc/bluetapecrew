var path = require('path');

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
                test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
                loader: 'file-loader',
                options: {
                    name: '../wwwroot/fonts/[name].[ext]'
                }
            },
            {
                test: /\.(png|jpe?g|gif)$/i,
                loader: 'file-loader',
                options: {
                    name: '../wwwroot/img/[name].[ext]'
                }
            }
        ]
    },
    entry: {
        app: './src/index.js'
    },
    output: {
        path: path.resolve(__dirname, "../wwwroot/"),
        filename: "bundle.js"
    }
};