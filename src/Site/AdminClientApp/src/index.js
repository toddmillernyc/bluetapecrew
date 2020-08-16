//import 'simple-line-icons/css/simple-line-icons.css';
import 'bootstrap/dist/css/bootstrap.css';
import 'jquery-uniform/themes/default/css/uniform.default.css';
import 'bootstrap-switch/dist/css/bootstrap3/bootstrap-switch.css';
import 'daterangepicker/daterangepicker.css';
import 'jqvmap/jqvmap/jqvmap.css';
import 'summernote/dist/summernote.css';
import '../assets/theme/css/layout.css';
import './index.css';
import { ProductEditor } from './modules/edit-product';

$ = window.$ = window.jQuery = require('jquery');
require('angular/angular.js');
require('angular-resource/angular-resource');
require('bootstrap');

window.vm = {
    saveAdditionImage: ProductEditor.saveAdditionImage,
    saveImageForm: ProductEditor.saveImageForm
}