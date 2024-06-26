import * as Yup from 'yup';

export const CreateAssetScheme = Yup.object().shape({
  name: Yup.string().required(''),
  category: Yup.string().required(''),
  specification: Yup.string().required(''),
  installedDate: Yup.date().required(''),
  state: Yup.string().required(''),
});

export interface ICreateAssetForm
  extends Yup.InferType<typeof CreateAssetScheme> {}
