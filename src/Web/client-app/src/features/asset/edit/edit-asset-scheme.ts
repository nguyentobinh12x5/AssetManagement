import * as Yup from 'yup';

export const EditAssetScheme = Yup.object().shape({
  name: Yup.string().required(''),
  specification: Yup.string().required(''),
  category: Yup.string().required(''),
  installedDate: Yup.string().required(''),
  state: Yup.string().required(''),
});

export interface IEditAssetForm extends Yup.InferType<typeof EditAssetScheme> {
  id: string;
}
